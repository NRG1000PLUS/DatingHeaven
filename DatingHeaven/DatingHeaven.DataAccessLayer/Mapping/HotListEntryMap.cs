﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatingHeaven.Entities;

namespace DatingHeaven.DataAccessLayer.Mapping {
    public class HotListEntryMap : BaseMap<HotListEntry>{
        public HotListEntryMap(){
            ToTable("HotList");

            // the composite key consists of the member who adds other member (target) to 
            // his hot list
            HasKey(entry => new{
                entry.MemberId,
                entry.TargetMemberId
            });

            Property(entry => entry.ShouldNotify)
                .IsRequired();

            Property(entry => entry.Comment)
                .IsOptional()
                .HasMaxLength(500);

            HasRequired(entry => entry.CurrentMember)
                .WithMany()
                .HasForeignKey(entry => entry.MemberId);

            HasRequired(entry => entry.TargetMember)
                .WithMany()
                .HasForeignKey(entry => entry.TargetMemberId);
        }
    }
}
