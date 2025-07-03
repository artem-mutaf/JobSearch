export function isValidEmail(email) {
    const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return re.test(email);
}

export function isValidPhone(phone) {
    const re = /^\+?\d{10,15}$/;
    return re.test(phone);
}

export function isNotEmpty(value) {
    return value && value.trim() !== '';
}

export function isStrongPassword(password) {
    return password.length >= 8;
}

//Функции для валидации пользовательского ввода, которые понадобятся при заполнении форм регистрации, входа, создания вакансий и т.д.