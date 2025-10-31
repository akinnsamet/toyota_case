import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import apiService from '../services/apiService';

const ProtectedRoutes = () => {
  const isAuthenticated = !!apiService.getAccessToken();

  return isAuthenticated ? <Outlet /> : <Navigate to="/login" replace />;
};

export default ProtectedRoutes;

