export const handleApiError = (error, defaultMessage) => {
  console.error(error);
  
  if (error.response) {
    
    const message = error.response.data?.message || defaultMessage;
    const details = error.response.data?.errors || null;
    
    return { 
      error: true,
      message,
      details,
      status: error.response.status
    };
  } else if (error.request) {
   
    return { 
      error: true,
      message: 'Сервер не отвечает. Пожалуйста, проверьте соединение.',
      status: 0
    };
  } else {
    
    return { 
      error: true,
      message: 'Ошибка при отправке запроса',
      details: error.message
    };
  }
};