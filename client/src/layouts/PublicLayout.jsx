import React from 'react';
import { Outlet } from 'react-router-dom';  // импорт Outlet
import Header from '../components/header/Header';
import Footer from '../components/footer/Footer';
import styles from './PublicLayout.module.scss';
import ChatButton from '../components/Chat/ChatButton';

function PublicLayout() {
  return (
    <div className={styles.layout}>
      <Header />
      <main className={styles.main}>
        <Outlet /> 
      </main>
      <Footer />
      <ChatButton />
    </div>
  );
}

export default PublicLayout;
