﻿namespace EPVV_CBR_RU.Enums
{
    /// <summary>
    /// Список возможных статусов сообщения
    /// </summary>
    public enum MessageStatus
    {
        /// <summary>
        /// <para>Статус в ЛК: Черновик</para>
        /// <para>Сообщение с данным статусом создано, но ещё не отправлено.</para>
        /// </summary>
        draft,
        /// <summary>
        /// <para>Статус в ЛК: Отправлено</para>
        /// <para>Сообщение получено сервером.</para>
        /// </summary>
        sent,
        /// <summary>
        /// <para>Статус в ЛК: Загружено</para>
        /// <para>Сообщение прошло первоначальную проверку.</para>
        /// </summary>
        delivered,
        /// <summary>
        /// <para>Статус в ЛК: Ошибка</para>
        /// <para>При обработке сообщения возникла ошибка.</para>
        /// </summary>
        error,
        /// <summary>
        /// <para>Статус в ЛК: Принято в обработку</para>
        /// <para>Сообщение передано во внутреннюю систему Банка России.</para>
        /// </summary>
        processing,
        /// <summary>
        /// <para>Статус в ЛК: Зарегистрировано</para>
        /// <para>Сообщение зарегистрировано.</para>
        /// </summary>
        registered,
        /// <summary>
        /// <para>Статус в ЛК: Отклонено</para>
        /// <para>Сообщение успешно дошло до получателя, но было отклонено.</para>
        /// </summary>
        rejected,
        /// <summary>
        /// <para>Статус в ЛК: Новое</para>
        /// <para>Только для входящих сообщений. Сообщение в данном статусе ещё не прочтено Пользователем УИО.</para>
        /// </summary>
        @new,
        /// <summary>
        /// <para>Статус в ЛК: Прочитано</para>
        /// <para>Только для входящих сообщений. Сообщение в данном статусе прочитано Пользователем УИО.</para>
        /// </summary>
        read,
        /// <summary>
        /// <para>Статус в ЛК: Отправлен ответ</para>
        /// <para>Только для входящих сообщений. На сообщение в данном статусе направлен ответ.</para>
        /// </summary>
        replied,
        /// <summary>
        /// <para>Статус в ЛК: Доставлено</para>
        /// <para>Сообщение успешно размещено в ЛК/Сообщение передано роутером во внутреннюю систему Банка России, от которой не ожидается ответ о регистрации.</para>
        /// </summary>
        succes
    }
}
