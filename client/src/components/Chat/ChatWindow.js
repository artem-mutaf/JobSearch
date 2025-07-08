import React, { useState, useEffect, useRef } from 'react';
import axios from 'axios';
import { 
  getUserChats, 
  getChatMessages, 
  sendMessage,
  createChat
} from '../../api/chats';
import ChatList from './ChatList'
import Message from './Message';
import styles from './ChatWindow.module.scss';

function ChatWindow({ userId, onClose, className }) {
  const [chats, setChats] = useState([]);
  const [selectedChatId, setSelectedChatId] = useState(null);
  const [messages, setMessages] = useState([]);
  const [input, setInput] = React.useState('');
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState(null);
  const messagesEndRef = useRef(null);

  
  useEffect(() => {
    const loadChats = async () => {
      setIsLoading(true);
      const result = await getUserChats(userId);
      console.log('Категории:', result)
      if (result.error) {
        setError(result.message);
      } else {
        setChats(result);
        if (result.length > 0) {
          setSelectedChatId(result[0].Id);
        }
      }
      setIsLoading(false);
    };

    loadChats();
  }, [userId]);

  
  useEffect(() => {
    if (!selectedChatId) return;

    const loadMessages = async () => {
      const result = await getChatMessages(selectedChatId);
      
      if (!result.error) {
        setMessages(result.messages || []);
      }
    };

    loadMessages();
  }, [selectedChatId]);

  
  useEffect(() => {
    messagesEndRef.current?.scrollIntoView({ behavior: 'smooth' });
  }, [messages]);

  const handleSend = async () => {
    if (!input.trim() || !selectedChatId) return;

    const newMessage = {
      text: input,
      senderId: userId,
      timestamp: new Date().toISOString()
    };

    
    setMessages(prev => [...prev, {
      ...newMessage,
      Id: `temp-${Date.now()}`,
      isOwn: true
    }]);
    setInput('');

    
    const result = await sendMessage(selectedChatId, newMessage);
    
    if (result.error) {
      
      setMessages(prev => prev.filter(m => !m.Id.startsWith('temp')));
      alert(result.message);
    } else {
      
      const updatedMessages = await getChatMessages(selectedChatId);
      if (!updatedMessages.error) {
        setMessages(updatedMessages.messages || []);
      }
    }
  };

  const handleKeyPress = (e) => {
    if (e.key === 'Enter') {
      handleSend();
    }
  };

  const handleCreateNewChat = async (employerId) => {
    const newChat = {
      participantIds: [userId, employerId]
    };
    
    const result = await createChat(newChat);
    
    if (!result.error) {
      setChats(prev => [...prev, result]);
      setSelectedChatId(result.Id);
    }
    
    return result;
  };

  const selectedChat = chats.find(chat => chat.Id === selectedChatId);

  if (isLoading) return <div className={styles.chatWindow}>Загрузка чатов...</div>;
  if (error) return <div className={styles.chatWindow}>{error}</div>;

  return (
    <div className={`${styles.chatWindow} ${className ? styles[className] : ''}`}>
       <button className={styles.closeButton} onClick={onClose}>×</button>

      <ChatList
        chats={chats}
        selectedChatId={selectedChatId}
        onSelectChat={setSelectedChatId}
      />

      <div className={styles.chatContent}>
        {selectedChat ? (
          <>
            <div className={styles.chatHeader}>
              {selectedChat.Name || `Чат #${selectedChat.Id}`}
            </div>

            <div className={styles.messages}>
              {messages.map((msg) => (
                <Message
                  key={msg.Id}
                  message={{
                    id: msg.Id,
                    text: msg.Text,
                    time: new Date(msg.Timestamp).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' }),
                    isOwn: msg.SenderId === userId
                  }}
                />
              ))}
              <div ref={messagesEndRef} />
            </div>

            <div className={styles.inputArea}>
              <input
                type="text"
                value={input}
                onChange={e => setInput(e.target.value)}
                onKeyPress={handleKeyPress}
                placeholder="Напишите сообщение..."
                disabled={!selectedChatId}
              />
              <button 
                onClick={handleSend}
                disabled={!input.trim() || !selectedChatId}
              >
                Отправить
              </button>
            </div>
          </>
        ) : (
          <div className={styles.noChatSelected}>
            <p>Выберите чат для начала общения</p>
            <button 
              onClick={() => handleCreateNewChat('some-employer-id')}
              className={styles.newChatButton}
            >
              Создать новый чат
            </button>
          </div>
        )}
      </div>
    </div>
  );
}

export default ChatWindow;
