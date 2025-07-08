import React, { useState, useEffect } from 'react';
import VacancyCard from '../../components/VacancyCard/VacancyCard';
import Pagination from '../../components/Pagination/Pagination';
import styles from './VacancyListPage.module.scss';
import { useLocation } from 'react-router-dom';
import { searchVacancies } from '../../api/vacancies';


const ITEMS_PER_PAGE = 2;



function VacancyListPage() {
  const [filterCategory, setFilterCategory] = useState('');
  const [currentPage, setCurrentPage] = useState(1);
  const [vacancies, setVacancies] = useState([]);
  const [loading, setLoading] = useState(true);

  const fetchVacancies = async () => {
    setLoading(true);
    try {
      const data = await searchVacancies({
        categoryId: filterCategory || null,
        pageNumber: currentPage,
        pageSize: ITEMS_PER_PAGE,
        sortBy: null,
        sortDescending: false,
      });

       console.log('Полученные вакансии:', data);
       
      setVacancies(data);
    } catch (error) {
      console.error('Ошибка при загрузке вакансий:', error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchVacancies();
  }, [filterCategory, currentPage]);

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
            setCurrentPage(1);
          }}
        >
          <option value="">Все</option>
          <option value="IT">IT</option>
          <option value="Продажи">Продажи</option>
          <option value="Администрация">Администрация</option>
        </select>
      </div>

      {loading ? (
        <p>Загрузка...</p>
      ) : (
        <>
          <div className={styles.vacancyList}>
            {vacancies.map(vacancy => (
              <VacancyCard key={vacancy.id} vacancy={vacancy} />
            ))}
          </div>

          <Pagination
            currentPage={currentPage}
            totalPages={Math.ceil(vacancies.length / ITEMS_PER_PAGE)} // возможно, придётся править, если бэк вернёт totalCount
            onPageChange={handlePageChange}
          />
        </>
      )}
    </div>
  );
}

export default VacancyListPage;
