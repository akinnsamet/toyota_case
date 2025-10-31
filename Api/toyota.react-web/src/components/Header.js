import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import apiService from '../services/apiService';
import { toast } from 'react-toastify';

const Header = () => {
  const navigate = useNavigate();

  const handleLogout = () => {
    if (window.confirm('Çıkış yapmak istediğinizden emin misiniz?')) {
      apiService.logout();
      toast.success('Çıkış yapıldı', { position: "bottom-right" });
      navigate('/login');
    }
  };

  return (
    <header className="navbar navbar-dark sticky-top flex-md-nowrap p-0 shadow" style={{ backgroundColor: '#9A616D' }}>
      <Link className="navbar-brand col-md-3 col-lg-2 me-0 px-3" to="/">
        Toyota Servis Yönetimi
      </Link>
      <div className="navbar-nav">
        <div className="nav-item text-nowrap">
          <button className="nav-link px-3 btn btn-link" onClick={handleLogout}>
            Çıkış Yap
          </button>
        </div>
      </div>
    </header>
  );
};

export default Header;
