﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TweeterSearchApp;

namespace TweeterSearchApp.Migrations
{
    [DbContext(typeof(TweetsDbContext))]
    [Migration("20191112004449_TweetIdRestored")]
    partial class TweetIdRestored
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TweeterSearchApp.Models.Tweet", b =>
                {
                    b.Property<decimal>("TweetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<float>("SentimentPolarization")
                        .HasColumnType("real");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TweetId");

                    b.ToTable("Tweets");
                });
#pragma warning restore 612, 618
        }
    }
}
