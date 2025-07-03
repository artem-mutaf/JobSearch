import React from 'react';
import styles from './Footer.module.scss';
import { Link } from 'react-router-dom';

function Footer() {
  return (
    <footer className={styles.footer}>
      <div className={styles.container}>
        <div className={styles.info}>
          <p>&copy; {new Date().getFullYear()} JobBoard. Все права защищены.</p>
        </div>
        <div className={styles.links}>
          <Link to="/vacancies">Вакансии</Link>
        </div>
      </div>
    </footer>
  );
}

export default Footer;
