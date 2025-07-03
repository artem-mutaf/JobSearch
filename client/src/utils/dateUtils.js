export function formatDate(date) {
  const d = new Date(date);
  return d.toLocaleDateString();
}

export function timeAgo(date) {
  const now = new Date();
  const diff = now - new Date(date);
  const seconds = Math.floor(diff / 1000);
  if (seconds < 60) return `${seconds} секунд назад`;
  const minutes = Math.floor(seconds / 60);
  if (minutes < 60) return `${minutes} минут назад`;
  const hours = Math.floor(minutes / 60);
  if (hours < 24) return `${hours} часов назад`;
  const days = Math.floor(hours / 24);
  return `${days} дней назад`;
}

//Функции для форматирования и работы с датами.