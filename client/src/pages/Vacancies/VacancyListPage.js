import React, { useState } from 'react';
import VacancyCard from '../../components/VacancyCard/VacancyCard';
import Pagination from '../../components/Pagination/Pagination';
import styles from './VacancyListPage.module.scss';

const vacanciesMock = [
  // Добавь больше элементов для демонстрации пагинации
  { id: 1, title: 'Frontend Developer', category: 'IT', salary: '1000$', description: 'Работа с React...' },
  { id: 2, title: 'Backend Developer', category: 'IT', salary: '1200$', description: 'Работа с .NET...' },
  { id: 3, title: 'Менеджер', category: 'Продажи', salary: '800$', description: 'Работа с клиентами...' },
  { id: 4, title: 'Дизайнер', category: 'IT', salary: '900$', description: 'Работа с UI/UX...' },
  { id: 5, title: 'QA Engineer', category: 'IT', salary: '950$', description: 'Тестирование...' },
  { id: 6, title: 'HR', category: 'Администрация', salary: '700$', description: 'Подбор персонала...' },
  // ... добавь сколько хочешь
];

const ITEMS_PER_PAGE = 2;

function VacancyListPage() {
  const [filterCategory, setFilterCategory] = useState('');
  const [currentPage, setCurrentPage] = useState(1);

  const filtered = filterCategory
    ? vacanciesMock.filter(v => v.category === filterCategory)
    : vacanciesMock;

  const totalPages = Math.ceil(filtered.length / ITEMS_PER_PAGE);

  const startIndex = (currentPage - 1) * ITEMS_PER_PAGE;
  const currentItems = filtered.slice(startIndex, startIndex + ITEMS_PER_PAGE);

  const handlePageChange = (page) => {
    setCurrentPage(page);
  };

  return (
    <div className={styles.container}>
      <h1>Вакансии</h1>
      <div className={styles.filters}>
        <label htmlFor="category">Категория:</label>
        <select
          id="category"
          value={filterCategory}
          onChange={e => {
            setFilterCategory(e.target.value);
            setCurrentPage(1); // сбросить на первую страницу при фильтрации
          }}
        >
          <option value="">Все</option>
          <option value="IT">IT</option>
          <option value="Продажи">Продажи</option>
          <option value="Администрация">Администрация</option>
        </select>
      </div>

      <div className={styles.vacancyList}>
        {currentItems.map(vacancy => (
          <VacancyCard key={vacancy.id} vacancy={vacancy} />
        ))}
      </div>

      <Pagination
        currentPage={currentPage}
        totalPages={totalPages}
        onPageChange={handlePageChange}
      />
    </div>
  );
}

export default VacancyListPage;
