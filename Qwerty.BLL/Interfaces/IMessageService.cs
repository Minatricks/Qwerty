﻿using Qwerty.BLL.DTO;
using Qwerty.BLL.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Qwerty.BLL.Interfaces
{
    public interface IMessageService
    {
        Task Send(MessageDTO messageDTO);
        MessageDTO GetMessage(int MessageID);
        Task DeleteMessage(int MessageID);
        Task<IEnumerable<MessageDTO>> GetLastMessages(string RecipientUserId);
        Task<IEnumerable<MessageDTO>> GetAllMessagesFromDialog(string SenderId, string RecepientId);
    }
}
