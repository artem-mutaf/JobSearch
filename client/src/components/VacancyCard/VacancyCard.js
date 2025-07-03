import React from 'react';
import { Link } from 'react-router-dom';
import styles from './VacancyCard.module.scss';

function VacancyCard({ vacancy }) {
  return (
    <Link to={`/vacancies/${vacancy.id}`} className={styles.cardLink}>
      <div className={styles.card}>
        <h3>{vacancy.title}</h3>
        <p><strong>Категория:</strong> {vacancy.category}</p>
        <p><strong>Зарплата:</strong> {vacancy.salary}</p>
        <p>{vacancy.description}</p>
      </div>
    </Link>
  );
}

export default VacancyCard;
