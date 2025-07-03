import React from 'react';
import styles from './ChatList.module.scss';

function ChatList({ chats, selectedChatId, onSelectChat }) {
  return (
    <div className={styles.chatList}>
      <h3>Диалоги</h3>
      <ul>
        {chats.map(chat => (
          <li
            key={chat.id}
            className={chat.id === selectedChatId ? styles.active : ''}
            onClick={() => onSelectChat(chat.id)}
          >
            {chat.name}
          </li>
        ))}
      </ul>
    </div>
  );
}

export default ChatList;
