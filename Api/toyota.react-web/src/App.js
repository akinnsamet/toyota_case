import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import Layout from './components/Layout';
import ProtectedRoutes from './components/ProtectedRoutes';
import LoginPage from './pages/LoginPage';
import HomePage from './pages/HomePage';
import ApplicationLogsPage from './pages/ApplicationLogsPage';
import VehicleServiceRecordLogsPage from './pages/VehicleServiceRecordLogsPage';

function App() {
  return (
    <Router>
      <ToastContainer />
      <Routes>
        <Route path="/login" element={<LoginPage />} />
        <Route element={<ProtectedRoutes />}>
          <Route
            path="/*"
            element={
              <Layout>
                <Routes>
                  <Route path="/" element={<HomePage />} />
                  <Route path="/application-logs" element={<ApplicationLogsPage />} />
                  <Route path="/vehicle-service-record-logs" element={<VehicleServiceRecordLogsPage />} />
                </Routes>
              </Layout>
            }
          />
        </Route>
      </Routes>
    </Router>
  );
}

export default App;
