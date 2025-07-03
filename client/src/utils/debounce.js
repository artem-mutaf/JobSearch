export function debounce(func, delay) {
  let timeoutId;
  return (...args) => {
    clearTimeout(timeoutId);
    timeoutId = setTimeout(() => {
      func.apply(null, args);
    }, delay);
  };
}

//Функция для оптимизации, например, при поиске — чтобы не отправлять запросы при каждом вводе символа.