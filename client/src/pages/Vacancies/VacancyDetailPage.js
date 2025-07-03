import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import styles from './VacancyDetailPage.module.scss'

function VacancyDetailPage() {
  const { id } = useParams(); // получаем id вакансии из URL
  const [vacancy, setVacancy] = useState(null);
  const [loading, setLoading] = useState(true);

  // Пока имитация загрузки вакансии (позже заменим на реальный API вызов)
  useEffect(() => {
    // Имитируем загрузку данных (заглушка)
    const fetchVacancy = async () => {
      setLoading(true);
      // Симуляция задержки
      await new Promise(r => setTimeout(r, 500));

      // Заглушка вакансии
      const dummyVacancy = {
        id,
        title: 'Frontend Developer',
        category: 'IT / Программирование',
        employmentType: 'Полная занятость',
        salary: 'от 120 000 руб.',
        description: 'Разработка и поддержка frontend части сайта, работа с React.js',
        employer: {
          name: 'Компания "Пример"',
          location: 'Москва',
          contactEmail: 'hr@example.com',
          phone: '+7 123 456 7890',
        },
      };

      setVacancy(dummyVacancy);
      setLoading(false);
    };

    fetchVacancy();
  }, [id]);

  if (loading) {
    return <div className={styles.loading}>Загрузка вакансии...</div>;
  }

  if (!vacancy) {
    return <div className={styles.error}>Вакансия не найдена</div>;
  }

  return (
    <div className={styles.vacancyDetail}>
      <h1 className={styles.title}>{vacancy.title}</h1>
      <div className={styles.info}>
        <p><strong>Категория:</strong> {vacancy.category}</p>
        <p><strong>Занятость:</strong> {vacancy.employmentType}</p>
        <p><strong>Оплата:</strong> {vacancy.salary}</p>
      </div>
      <div className={styles.description}>
        <h2>Описание вакансии</h2>
        <p>{vacancy.description}</p>
      </div>
      <div className={styles.employer}>
        <h2>Работодатель</h2>
        <p><strong>Название:</strong> {vacancy.employer.name}</p>
        <p><strong>Локация:</strong> {vacancy.employer.location}</p>
        <p><strong>Контакты:</strong></p>
        <ul>
          <li>Email: {vacancy.employer.contactEmail}</li>
          <li>Телефон: {vacancy.employer.phone}</li>
        </ul>
      </div>
    </div>
  );
}

export default VacancyDetailPage;
