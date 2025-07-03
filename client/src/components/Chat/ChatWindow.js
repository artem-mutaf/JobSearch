import React, { useState, useEffect, useRef } from 'react';
import Message from './Message';
import styles from './ChatWindow.module.scss';

function ChatWindow({ chat }) {
  const [input, setInput] = React.useState('');

  const handleSend = () => {
    if (!input.trim()) return;
    alert(`Отправлено: ${input}`);
    setInput('');
  };

  return (
    <div className={styles.chatWindow}>
      <h3>{chat?.name || 'Выберите чат'}</h3>
      <div className={styles.messages}>
        {(chat?.messages || []).map(msg => (
          <Message key={msg.id} message={msg} isOwn={msg.isOwn} />
        ))}
      </div>
      <div className={styles.inputArea}>
        <input
          type="text"
          value={input}
          onChange={e => setInput(e.target.value)}
          placeholder="Напишите сообщение..."
        />
        <button onClick={handleSend}>Отправить</button>
      </div>
    </div>
  );
}

export default ChatWindow;
