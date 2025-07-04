using JobBoard.Core.Enums;
using JobBoard.Core.ValueObjects;

namespace JobBoard.Core.Entities;

/// <summary>
    /// Представляет работодателя, который публикует вакансии.
    /// </summary>
    public class Employer
    {
        /// <summary>
        /// Уникальный идентификатор работодателя.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Электронная почта работодателя (уникальная, обязательная).
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Тип лица (физическое или юридическое).
        /// </summary>
        public EntityType EntityType { get; set; }

        /// <summary>
        /// Описание деятельности компании (обязательное, максимум 1000 символов).
        /// </summary>
        public string CompanyDescription { get; set; }

        /// <summary>
        /// Дополнительная информация о компании (опционально, максимум 2000 символов).
        /// </summary>
        public string About { get; set; }

        /// <summary>
        /// Контактная информация работодателя (email, телефон, соцсети).
        /// </summary>
        public ContactInfo ContactInfo { get; set; }

        /// <summary>
        /// Местоположение работодателя (адрес, регион).
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// Хэшированный пароль работодателя.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Флаг, указывающий, подтверждена ли почта работодателя.
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        /// Список категорий, к которым относится работодатель.
        /// </summary>
        public List<Category> Categories { get; set; } = new();

        /// <summary>
        /// URL изображения компании (опционально).
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Список вакансий, созданных работодателем.
        /// </summary>
        public List<Vacancy> Vacancies { get; set; } = new();

        /// <summary>
        /// Список чатов, в которых участвует работодатель.
        /// </summary>
        public List<Chat> Chats { get; set; } = new();
    }