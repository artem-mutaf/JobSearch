import React from 'react';
import { Routes, Route } from 'react-router-dom';

import PublicLayout from '../layouts/PublicLayout';

import HomePage from '../pages/Home/HomePage';
import VacancyListPage from '../pages/Vacancies/VacancyListPage';
import VacancyDetailPage from '../pages/Vacancies/VacancyDetailPage';  // импортируем
import LoginPage from '../pages/Auth/LoginPage';
import RegisterPage from '../pages/Auth/RegisterPage';
import ChatPage from '../pages/Chat/ChatPage';

function AppRoutes() {
  return (
    <Routes>
      <Route element={<PublicLayout />}>
        <Route path="/" element={<HomePage />} />
        <Route path="/vacancies" element={<VacancyListPage />} />
        <Route path="/vacancies/:id" element={<VacancyDetailPage />} /> {/* добавили */}
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/chat" element={<ChatPage />}/>
      </Route>
    </Routes>
  );
}

export default AppRoutes;
