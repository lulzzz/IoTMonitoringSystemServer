﻿using Microsoft.EntityFrameworkCore;
using MonitoringSystem.Data;
using MonitoringSystem.Extensions;
using MonitoringSystem.Models;
using MonitoringSystem.Persistences.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MonitoringSystem.Persistences.Repositories
{
    public class RackRepository : IRackRepository
    {
        private ApplicationDbContext context;

        public RackRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddRack(Rack rack)
        {
            context.Racks.Add(rack);
        }

        public async Task<Rack> GetRack(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Racks.FindAsync(id);
            }
            return await context.Racks
                .Include(r => r.Sensor)
                .Include(r => r.Room)
                .SingleOrDefaultAsync(r => r.RackId == id);
        }

        public async Task<QueryResult<Rack>> GetRacks(Query queryObj)
        {
            var result = new QueryResult<Rack>();
            var query = context.Racks
                    .Where(r => r.IsDeleted == false)
                    .Include(r => r.Room)
                    .Include(r => r.Sensor)
                    .AsQueryable();

            //filter
            if (queryObj.RoomId.HasValue)
            {
                query = query.Where(q => q.Room.RoomId == queryObj.RoomId);
            }

            //sort
            var columnsMap = new Dictionary<string, Expression<Func<Rack, object>>>()
            {
                ["rackcode"] = s => s.RackCode,
                ["rackname"] = s => s.RackName
            };
            if (queryObj.SortBy != "id" || queryObj.IsSortAscending != true)
            {
                query = query.OrderByDescending(s => s.RackId);
            }
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }

        public void RemoveRack(Rack rack)
        {
            rack.IsDeleted = true;
        }

        public void AddRackLog(Rack rack)
        {
            rack.Logs.Add(new Log
            {
                DateTime = DateTime.Now,
                Description = "rack name: " + rack.RackName + ", rack code: " + rack.RackCode +
                 " was added into " + rack.Room.RoomName + "."
            });
        }

        public void UpdateRackLog(Rack oldRack, Rack rack)
        {
            rack.Logs.Add(new Log
            {
                DateTime = DateTime.Now,
                Description = "rack name change from: " + oldRack.RackName + " to " + rack.RackName +
                ", rack code change from: " + oldRack.RackCode + " to " + rack.RackCode +
                ", room change from " + oldRack.Room.RoomName + " to " + rack.Room.RoomName + "."
            });
        }
    }
}
