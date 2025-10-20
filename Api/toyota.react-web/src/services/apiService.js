const API_BASE_URL = process.env.REACT_APP_API_BASE_URL || 'http://localhost:5006';

const apiService = {
  login: async (username, password) => {
    try {
      const response = await fetch(`${API_BASE_URL}/api/Auth/Login`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ username, password }),
      });

      const data = await response.json(); 

      if (data && data.Result && data.Result.ResultCode === 0 && data.ReturnObject?.AccessToken) {
        localStorage.setItem('accessToken', data.ReturnObject.AccessToken);
        localStorage.setItem('username', data.ReturnObject.User?.Username || '');
        return data;
      } else {
        return data || { Result: { ResultCode: -1, ResultMessage: 'Beklenmeyen API yanıtı veya giriş başarısız.' } };
      }
    } catch (error) {
      console.error('Login API çağrısı sırasında hata oluştu:', error);
      return { Result: { ResultCode: -1, ResultMessage: 'API bağlantı hatası' } };
    }
  },

  logout: () => {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('username');
    window.location.href = '/login';
  },

  getAccessToken: () => {
    return localStorage.getItem('accessToken');
  },

  callApi: async (endpoint, method = 'GET', body = null, requiresAuth = true) => {
    const headers = {
      'Content-Type': 'application/json',
    };

    if (requiresAuth) {
      const token = apiService.getAccessToken();
      if (!token) {
        apiService.logout();
        return null; 
      }
      headers['Authorization'] = `Bearer ${token}`;
    }

    try {
      const config = {
        method,
        headers,
      };

      if (body) {
        config.body = JSON.stringify(body);
      }

      const response = await fetch(`${API_BASE_URL}${endpoint}`, config);

      if (response.status === 401) {
        apiService.logout();
        return null;
      }

      return await response.json();
    } catch (error) {
      console.error(`API çağrısı sırasında hata oluştu (${endpoint}):`, error);
      return { Result: { ResultCode: -1, ResultMessage: 'API bağlantı hatası' } };
    }
  },

  getVehicleServiceRecords: async (request) => {
    return apiService.callApi('/api/VehicleServiceRecord/GetAll', 'POST', request);
  },

  getVehicleServiceRecord: async (id) => {
    return apiService.callApi(`/api/VehicleServiceRecord?id=${id}`);
  },

  createVehicleServiceRecord: async (record) => {
    return apiService.callApi('/api/VehicleServiceRecord', 'POST', record);
  },

  updateVehicleServiceRecord: async (record) => {
    return apiService.callApi('/api/VehicleServiceRecord', 'PUT', record);
  },

  deleteVehicleServiceRecord: async (id) => {
    return apiService.callApi(`/api/VehicleServiceRecord?id=${id}`, 'DELETE');
  },

  getCities: async () => {
    return apiService.callApi('/api/City');
  },

  getApplicationLogs: async (request) => {
    return apiService.callApi('/api/ApplicationLog/GetAll', 'POST', request);
  },

  getVehicleServiceRecordLogs: async (request) => {
    return apiService.callApi('/api/VehicleServiceRecord/GetLogAll', 'POST', request);
  },
};

export default apiService;
