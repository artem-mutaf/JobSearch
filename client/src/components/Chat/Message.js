import React from 'react';
import styles from './Message.module.scss';

function Message({ message }) {
  return (
    <div className={`${styles.message} ${message.isOwn ? styles.own : styles.other}`}>
      <div className={styles.content}>
        <div className={styles.text}>{message.text}</div>
        <div className={styles.time}>{message.time}</div>
      </div>
    </div>
  );
}

export default Message;