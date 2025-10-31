import React from 'react';
import { Link } from 'react-router-dom';

const Sidebar = () => {
  return (
    <nav id="sidebarMenu" className="col-md-3 col-lg-2 d-md-block bg-light sidebar collapse">
      <div className="position-sticky pt-3">
        <ul className="nav flex-column">
          <li className="nav-item">
            <Link className="nav-link" to="/" style={{ color: 'black' }}>
              <i className="fas fa-car" style={{ color: 'black' }}></i>  Araç Servis Kayıt
            </Link>
          </li>
          <li className="nav-item">
            <Link className="nav-link" to="/vehicle-service-record-logs" style={{ color: 'black' }}>
              <i className="fas fa-clipboard-list" style={{ color: 'black' }}></i>  Araç Servis Kayıt Log
            </Link>
          </li>
          <li className="nav-item">
            <Link className="nav-link" to="/application-logs" style={{ color: 'black' }}>
              <i className="fas fa-file-alt" style={{ color: 'black' }}></i>  Uygulama Logları
            </Link>
          </li>
        </ul>
      </div>
    </nav>
  );
};

export default Sidebar;
