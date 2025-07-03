import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import styles from './Auth.module.scss';

function LoginPage() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  return (
    <div className={styles.pageWrapper}>
      <div className={styles.authContainer}>
      <h2>Вход</h2>
      <form className={styles.form}>
        <div className={styles.formGroup}>
            <input
              type="email"
              id="email"
              value={email}
              onChange={e => setEmail(e.target.value)}
              placeholder=" "
              required
            />
            <label htmlFor="email">Email</label>
          </div>

          <div className={styles.formGroup}>
            <input
              type="password"
              id="password"
              value={password}
              onChange={e => setPassword(e.target.value)}
              placeholder=" "
              required
            />
            <label htmlFor="password">Пароль</label>
        </div>

        <button type="submit" className={styles.submitBtn}>
          Войти
        </button>
      </form>

      <div className={styles.registerPrompt}>
        <p>Нет аккаунта?&nbsp;
          <Link to="/register" className={styles.registerLink}>
            Зарегистрироваться
          </Link>
        </p>
      </div>
    </div>
    </div>
    
  );
}

export default LoginPage;
