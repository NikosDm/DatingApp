using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingAppApi.DTOs;
using DatingAppApi.Entities;
using DatingAppApi.Helpers;
using DatingAppApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DatingAppApi.Data
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public MessageRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void DeleteMessage(Message message)
        {
            _context.Messages.Remove(message);
        }

        public void AddGroup(Group group)
        {
            _context.Groups.Add(group);
        }

        public async Task<Connection> GetConnection(string connectionId)
        {
            return await _context.Connections.FindAsync(connectionId);
        }

        public async Task<Group> GetMessageGroup(string groupName)
        {
            return await _context.Groups.Include(x => x.Connections).FirstOrDefaultAsync(x => x.Name == groupName);
        }

        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
        }

        public async Task<Message> GetMessage(int id)
        {
            return await _context.Messages
                .Include(u => u.Sender)
                .Include(u => u.Recipient)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PagedList<MessageDTO>> GetMessagesForUser(MessageParams messageParams)
        {
            var query = _context.Messages
                .OrderByDescending(x => x.MessageSent)
                .ProjectTo<MessageDTO>(_mapper.ConfigurationProvider)
                .AsQueryable();

            query = messageParams.Container switch 
            {
                "Inbox" => query.Where(u => u.RecipientUsername == messageParams.Username && u.RecipientDeleted == false),
                "Outbox" => query.Where(u => u.SenderUsername == messageParams.Username && u.SenderDeleted == false),
                _ => query.Where(u => u.RecipientUsername == messageParams.Username && u.RecipientDeleted == false && u.DateRead == null)
            };

            return await PagedList<MessageDTO>.CreateAsync(query, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<IEnumerable<MessageDTO>> GetMessageThread(string currentUsername, string recipientUsername)
        {
            var messages = await _context.Messages
                .Where(m => m.Recipient.UserName == currentUsername && m.RecipientDeleted == false
                    && m.Sender.UserName == recipientUsername
                    || m.Recipient.UserName == recipientUsername
                    && m.Sender.UserName == currentUsername && m.SenderDeleted == false
                )
                .OrderBy(m => m.MessageSent)
                .ProjectTo<MessageDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            var unreadMessages = messages.Where(m => m.DateRead == null && m.RecipientUsername == currentUsername).ToList();

            if (unreadMessages.Any()) 
            {
                foreach (var message in unreadMessages) 
                {
                    message.DateRead = DateTime.UtcNow;
                }
            }

            return messages;
        }

        public void RemoveConnection(Connection connection)
        {
            _context.Connections.Remove(connection);
        }

        public async Task<Group> GetGroupForConnection(string connectionId)
        {
            return await _context.Groups
                .Include(c => c.Connections)
                .Where(c => c.Connections.Any(x => x.ConnectionId == connectionId))
                .FirstOrDefaultAsync();
        }
    }
}