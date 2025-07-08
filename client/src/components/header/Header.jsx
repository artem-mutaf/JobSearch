import React from 'react';
import styles from './Header.module.scss';
import { Link } from 'react-router-dom';

function Header() {
  return (
    <header className={styles.header}>
      <div className={styles.container}>
        <Link to="/" className={styles.logo}>JobBoard</Link>
        <nav className={styles.nav}>
          <Link to="/vacancies">Вакансии</Link>
          <Link to="/login">Войти</Link>
        </nav>
      </div>
    </header>
  );
}

export default Header;
