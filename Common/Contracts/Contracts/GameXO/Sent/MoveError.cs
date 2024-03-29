﻿
using Contracts.GameXO.Enums;
using Contracts.GameXO.Models;

namespace Contracts.GameXO.Sent
{
    /// <summary>
    /// Информация об ошибке при ходе
    /// </summary>
    public class MoveError : MoveMessageIdentifiers
    {
        /// <summary>
        /// Информация об ошибке
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Тип ошибки
        /// </summary>
        public ErrorType Type { get; set; }
    }
}
