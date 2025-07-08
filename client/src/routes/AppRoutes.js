import React from 'react';
import { Routes, Route } from 'react-router-dom';

import PublicLayout from '../layouts/PublicLayout';

import HomePage from '../pages/Home/HomePage';
import VacancyListPage from '../pages/Vacancies/VacancyListPage';
import VacancyDetailPage from '../pages/Vacancies/VacancyDetailPage';
import LoginPage from '../pages/Auth/LoginPage';
import RegisterPage from '../pages/Auth/RegisterPage';

function AppRoutes() {
  return (
    <Routes>
      <Route element={<PublicLayout />}>
        <Route path="/" element={<HomePage />} />
        <Route path="/vacancies" element={<VacancyListPage />} />
        <Route path="/vacancies/:id" element={<VacancyDetailPage />} /> 
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
      </Route>
    </Routes>
  );
}

export default AppRoutes;
