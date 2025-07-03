import React, { useState } from 'react';
import ChatList from '../../components/Chat/ChatList';
import ChatWindow from '../../components/Chat/ChatWindow';
import styles from './ChatPage.module.scss';

const mockChats = [
  {
    id: 1,
    name: 'Работодатель: ООО "Ромашка"',
    messages: [
      { id: 1, text: 'Здравствуйте!', time: '10:00', isOwn: false },
      { id: 2, text: 'Здравствуйте, интересуюсь вакансией.', time: '10:05', isOwn: true },
    ],
  },
  {
    id: 2,
    name: 'Работодатель: ИП Иванов',
    messages: [
      { id: 1, text: 'Добрый день!', time: '11:00', isOwn: false },
      { id: 2, text: 'Добрый день, расскажите о работе.', time: '11:10', isOwn: true },
    ],
  },
];

function ChatPage() {
  const [selectedChatId, setSelectedChatId] = useState(mockChats[0].id);
  const selectedChat = mockChats.find(c => c.id === selectedChatId);

  return (
    <div className={styles.chatPage}>
      <ChatList
        chats={mockChats}
        selectedChatId={selectedChatId}
        onSelectChat={setSelectedChatId}
      />
      <ChatWindow chat={selectedChat} />
    </div>
  );
}

export default ChatPage;
