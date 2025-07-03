import React from 'react';
import styles from './HomePage.module.scss';
import { Link } from 'react-router-dom';

function HomePage() {
  return (
    <div className={styles.home}>
      {/* Hero-секция */}
      <section className={styles.hero}>
        <div className={styles.heroContent}>
          <h1>Найдите работу своей мечты</h1>
          <p>Тысячи вакансий от проверенных работодателей</p>
          <Link to="/vacancies" className={styles.button}>Поиск вакансий</Link>
        </div>
      </section>

      {/* Популярные категории */}
      <section className={styles.categories}>
        <h2>Популярные категории</h2>
        <div className={styles.grid}>
          <div className={styles.categoryCard}>IT и Программирование</div>
          <div className={styles.categoryCard}>Маркетинг</div>
          <div className={styles.categoryCard}>Образование</div>
          <div className={styles.categoryCard}>Медицина</div>
        </div>
      </section>

      {/* Преимущества */}
      <section className={styles.benefits}>
        <h2>Почему выбирают нас?</h2>
        <ul>
          <li>✔️ Простая регистрация</li>
          <li>✔️ Быстрый отклик на вакансию</li>
          <li>✔️ Удобный чат с работодателем</li>
          <li>✔️ Только актуальные предложения</li>
        </ul>
      </section>

      {/* Призыв к действию */}
      <section className={styles.cta}>
        <h2>Готовы начать?</h2>
        <p>Создайте аккаунт работодателя и опубликуйте первую вакансию бесплатно</p>
        <Link to="/register" className={styles.button}>Регистрация</Link>
      </section>
    </div>
  );
}

export default HomePage;
