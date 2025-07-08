import axios from 'axios';
import { handleApiError } from './errorHandler';

const API_BASE_URL = 'https://a86a-217-19-215-68.ngrok-free.app/api/chats';


export const createChat = async (chatData) => {
  try {
    const response = await axios.post(API_BASE_URL, chatData, {
        headers: {
        'ngrok-skip-browser-warning': 'true'
    }
    });
    console.log('Ответ с сервера:', response.data);
    return response.data;
  } catch (error) {
    return handleApiError(error, 'Ошибка при создании чата');
  }
};


export const getChatById = async (chatId) => {
  try {
    const response = await axios.get(`${API_BASE_URL}/${chatId}`, {
        headers: {
        'ngrok-skip-browser-warning': 'true'
    }
    });
    console.log('Ответ с сервера:', response.data);
    return response.data;
  } catch (error) {
    return handleApiError(error, 'Ошибка при получении чата');
  }
};

export const getUserChats = async () => {
  const testUserId = '33333333-3333-3333-3333-333333333333'; // ID из вашего API
  const API_URL = 'https://a86a-217-19-215-68.ngrok-free.app/api/chats/user'; // Базовый URL

  try {
    console.log('[TEST] Отправляем запрос чатов для пользователя:', testUserId);
    
    const response = await axios.get(`${API_URL}/${testUserId}`, {
       headers: {
        'ngrok-skip-browser-warning': 'true'
    }
    });

    console.group('[TEST] Успешный ответ от сервера');
    console.log('Статус:', response.status);
    console.log('Данные:', response.data);
    
    
    if (Array.isArray(response.data)) {
      console.log('Количество чатов:', response.data.length);
      response.data.forEach((chat, index) => {
        console.group(`Чат #${index + 1}`);
        console.log('ID чата:', chat.Id);
        console.log('ID заявки:', chat.ApplicationId);
        console.log('ID работодателя:', chat.EmployerId);
        console.log('ID вакансии:', chat.VacancyId);
        console.groupEnd();
      });
    } else {
      console.warn('Данные не являются массивом!');
    }
    console.groupEnd();

    return response.data;
  } catch (error) {
    console.group('[TEST] Ошибка запроса');
    console.error('URL:', error.config?.url);
    console.error('Статус:', error.response?.status);
    console.error('Данные ошибки:', error.response?.data);
    console.error('Сообщение:', error.message);
    console.groupEnd();
    
    throw error;
  }
};


export const deleteChat = async (chatId) => {
  try {
    await axios.delete(`${API_BASE_URL}/${chatId}`, {
        headers: {
        'ngrok-skip-browser-warning': 'true'
    }
    });
    return true;
  } catch (error) {
    return handleApiError(error, 'Ошибка при удалении чата');
  }
};


export const sendMessage = async (chatId, messageData) => {
  try {
    const response = await axios.post(`${API_BASE_URL}/${chatId}/messages`, messageData, {
        headers: {
        'ngrok-skip-browser-warning': 'true'
    }
    });
    console.log('Ответ с сервера:', response.data);
    return response.data;
  } catch (error) {
    return handleApiError(error, 'Ошибка при отправке сообщения');
  }
};


export const getChatMessages = async (chatId) => {
  try {
    const response = await axios.get(`${API_BASE_URL}/${chatId}/messages`, {
        headers: {
        'ngrok-skip-browser-warning': 'true'
    }
    });
    console.log('Ответ с сервера:', response.data);
    return response.data;
  } catch (error) {
    return handleApiError(error, 'Ошибка при получении сообщений');
  }
};


export const markAsRead = async (chatId, messageIds) => {
  try {
    await axios.patch(`${API_BASE_URL}/${chatId}/read`, { messageIds }, {
        headers: {
        'ngrok-skip-browser-warning': 'true'
    }
    });
    return true;
  } catch (error) {
    return handleApiError(error, 'Ошибка при обновлении статуса сообщений');
  }
};