import React from 'react';
import { Link } from 'react-router-dom';
import styles from './VacancyCard.module.scss';

function VacancyCard({ vacancy }) {
  return (
    <Link to={`/vacancies/${vacancy.id}`} className={styles.cardLink}>
      <div className={styles.card}>
        <h3>{vacancy.title}</h3>
        <p><strong>Категория:</strong> {vacancy.CategoryId}</p>
        <p><strong>{vacancy.Title}</strong> </p>
        <p><strong>Время объявления вакансии:</strong> {vacancy.CreatedAt}</p>
        <p><strong>Зарплата:</strong> {vacancy.Salary}</p>
        <p>{vacancy.Description}</p>
      </div>
    </Link>
  );
}

export default VacancyCard;
