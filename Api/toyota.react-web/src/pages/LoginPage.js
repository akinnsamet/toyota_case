import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import apiService from '../services/apiService';

const LoginPage = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    if (apiService.getAccessToken()) {
      navigate('/');
    }
  }, [navigate]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const response = await apiService.login(username, password);
    if (response?.Result?.ResultCode === 0) {
      toast.success(response.Result?.ResultMessage || 'Giriş başarılı', { position: "bottom-right" });
      navigate('/');
    } else {
      toast.error(response.Result?.ResultMessage || 'Giriş başarısız', { position: "bottom-right" });
    }
  };

  return (
    <section className="vh-100" style={{ backgroundColor: '#9A616D' }}>
      <div className="container py-5 h-100">
        <div className="row d-flex justify-content-center align-items-center h-100">
          <div className="col col-xl-10">
            <div className="card" style={{ borderRadius: '1rem' }}>
              <div className="row g-0">
                <div className="col-md-6 col-lg-5 d-none d-md-flex align-items-center justify-content-center">
                  <img src="/images/login_car_image.png" alt="login form" className="img-fluid" style={{ borderRadius: '1rem 0 0 1rem' }} />
                </div>
                <div className="col-md-6 col-lg-7 d-flex align-items-center">
                  <div className="card-body p-4 p-lg-5 text-black">
                    <form onSubmit={handleSubmit}>
                      <div className="d-flex align-items-center mb-3 pb-1">
                        <img src="/images/toyotaicon.svg" className="fa-2x me-3" alt="Phone image" style={{ width: '50px' }} />
                        <span className="h1 fw-bold mb-0">Toyota Servis Yönetimi</span>
                      </div>
                      <h5 className="fw-normal mb-3 pb-3" style={{ letterSpacing: '1px' }}>Hesabınıza giriş yapın</h5>
                      <div className="form-outline mb-4">
                        <input
                          type="text"
                          id="username"
                          className="form-control form-control-lg"
                          value={username}
                          onChange={(e) => setUsername(e.target.value)}
                          required
                        />
                        <label className="form-label" htmlFor="username">Kullanıcı Adı</label>
                      </div>
                      <div className="form-outline mb-4">
                        <input
                          type="password"
                          id="password"
                          className="form-control form-control-lg"
                          value={password}
                          onChange={(e) => setPassword(e.target.value)}
                          required
                        />
                        <label className="form-label" htmlFor="password">Şifre</label>
                      </div>
                      <div className="pt-1 mb-4">
                        <button className="btn btn-dark btn-lg btn-block" type="submit">Giriş Yap</button>
                      </div>
                    </form>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};

export default LoginPage;
