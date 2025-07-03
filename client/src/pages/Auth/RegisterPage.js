import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import styles from './Register.module.scss';

function RegisterPage() {
  const [role, setRole] = useState('employer');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');

  return (
    <div className={styles.pageWrapper}>
      <div className={styles.authContainer}>
        <h2>Регистрация</h2>
        <form className={styles.form}>
          <div className={styles.formGroup}>
            <select
              id="role"
              value={role}
              onChange={e => setRole(e.target.value)}
              placeholder=" "
            >
              <option value="employer">Работодатель</option>
              <option value="applicant">Соискатель</option>
            </select>
            <label htmlFor="role"></label>
          </div>

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

          <div className={styles.formGroup}>
            <input
              type="password"
              id="confirmPassword"
              value={confirmPassword}
              onChange={e => setConfirmPassword(e.target.value)}
              placeholder=" "
              required
            />
            <label htmlFor="confirmPassword">Подтвердите пароль</label>
          </div>

          {/* Дополнительные поля для работодателя */}
          {role === 'employer' && (
            <div className={styles.roleFields}>
              <div className={styles.formGroup}>
                <input type="text" id="companyName" placeholder=" " />
                <label htmlFor="companyName">Название компании</label>
              </div>
              <div className={styles.formGroup}>
                <input type="text" id="companyLocation" placeholder=" " />
                <label htmlFor="companyLocation">Локация</label>
              </div>
            </div>
          )}

          {/* Дополнительные поля для соискателя */}
          {role === 'applicant' && (
            <div className={styles.roleFields}>
              <div className={styles.formGroup}>
                <input type="text" id="fullName" placeholder=" " />
                <label htmlFor="fullName">ФИО</label>
              </div>
              <div className={styles.formGroup}>
                <input type="text" id="activity" placeholder=" " />
                <label htmlFor="activity">Род деятельности</label>
              </div>
            </div>
          )}

          
          <button type="submit" className={styles.submitBtn}>
            Зарегистрироваться
          </button>
        </form>

        <div className={styles.loginPrompt}>
          <p>
            Уже есть аккаунт?&nbsp;
            <Link to="/login" className={styles.loginLink}>
              Войти
            </Link>
          </p>
        </div>
        
      </div>
    </div>
  );
}

export default RegisterPage;
