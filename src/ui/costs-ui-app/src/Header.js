import React from 'react';
import { Link } from 'react-router-dom';
import './Header.css';

function Header() {
  return (
    <div className="header-container">
      <div>
        <Link className='header-link' to="/">Home</Link>
        <Link className='header-link' to="/products">Products</Link>
        <Link className='header-link' to="/checks">Checks</Link>
      </div>
    </div>
  );
}

export default Header;
