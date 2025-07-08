import React, { useState, useEffect } from 'react';
import styles from './HomePage.module.scss';
import { Link, useNavigate } from 'react-router-dom';
import { getAllCategories } from '../../api/categories'

function HomePage() {
  const[searchQuery, setSearchQuery] = useState('');
  const [categories, setCategories] = useState([]);
  const navigate = useNavigate();

  const handleSearch = (e) => {
    e.preventDefault();
    if (searchQuery.trim()) {
      navigate(`/vacancies?query=${encodeURIComponent(searchQuery.trim())}`)
    }
  };

  useEffect(() => {
    async function fetchCategories() {
      try {
        const data = await getAllCategories();
        console.log('Категории:', data)
        setCategories(data);
      } catch (error) {
        console.error('Ошибка при загрузке категорий:', error);
      }
    }

    fetchCategories();
  }, []);

  return (
    <div className={styles.home}>
      <section className={styles.hero}>
        <div className={styles.heroContent}>
          <h1>Найдите работу своей мечты</h1>
          <p>Тысячи вакансий от проверенных работодателей</p>
          <Link to="/vacancies" className={styles.button}>Поиск вакансий</Link>

          <form onSubmit={handleSearch} className={styles.searchForm}>
            <input
              type='text'
              placeholder='Профессия,должность или компания'
              value={searchQuery}
              onChange={(e) => setSearchQuery(e.target.value)}
            />
            <button type='submit'>Найти вакансию</button>
          </form>
        </div>
      </section>

      
      <section className={styles.categories}>
        <h2>Популярные категории</h2>
        <div className={styles.grid}>
                {categories?.length ? (
                categories.map((cat) => (
                  <Link 
                    to={`/vacancies?category=${cat.Id}`} 
                    key={cat.Id} 
                    className={styles.categoryCard}
                  >
                    {cat.Name}
                  </Link>
                ))
              ) : (
                <p>Категории загружаются...</p>
              )}
        </div>
      </section>

      
      <section className={styles.benefits}>
        <h2>Почему выбирают нас?</h2>
        <ul>
          <li>✔️ Простая регистрация</li>
          <li>✔️ Быстрый отклик на вакансию</li>
          <li>✔️ Удобный чат с работодателем</li>
          <li>✔️ Только актуальные предложения</li>
        </ul>
      </section>

      
      <section className={styles.cta}>
        <h2>Готовы начать?</h2>
        <p>Создайте аккаунт работодателя и опубликуйте первую вакансию бесплатно</p>
        <Link to="/register" className={styles.button}>Регистрация</Link>
      </section>
    </div>
  );
}

export default HomePage;
