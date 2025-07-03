import React from 'react';
import { Outlet } from 'react-router-dom';  // импорт Outlet
import Header from '../components/header/Header';
import Footer from '../components/footer/Footer';
import styles from './PublicLayout.module.scss';

function PublicLayout() {
  return (
    <div className={styles.layout}>
      <Header />
      <main className={styles.main}>
        <Outlet />  {/* вот сюда React Router подставит текущий маршрут */}
      </main>
      <Footer />
    </div>
  );
}

export default PublicLayout;
