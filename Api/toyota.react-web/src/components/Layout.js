import React from 'react';
import Header from './Header';
import Sidebar from './Sidebar';

const Layout = ({ children }) => {
  const isAuthPage = window.location.pathname === '/login';

  return (
    <>
      {!isAuthPage && <Header />}
      <div className={isAuthPage ? "" : "container-fluid"}>
        <div className="row">
          {!isAuthPage && <Sidebar />}
          <main role="main" className={isAuthPage ? "col-12" : "col-md-9 ms-sm-auto col-lg-10 px-md-4"}>
            {children}
          </main>
        </div>
      </div>
    </>
  );
};

export default Layout;

