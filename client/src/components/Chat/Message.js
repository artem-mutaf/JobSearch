import React from 'react';
import styles from './Message.module.scss';

function Message({ message, isOwn }) {
  return (
    <div className={`${styles.message} ${isOwn ? styles.own : ''}`}>
      <div className={styles.text}>{message.text}</div>
      <div className={styles.time}>{message.time}</div>
    </div>
  );
}

export default Message;
