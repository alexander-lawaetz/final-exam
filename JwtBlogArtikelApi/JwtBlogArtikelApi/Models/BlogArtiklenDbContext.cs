﻿using Microsoft.EntityFrameworkCore;
using JwtBlogArtikelApi.Models;

namespace JwtBlogArtikelApi.Models
{
    public class BlogArtiklenDbContext : DbContext
    {
        public BlogArtiklenDbContext(DbContextOptions<BlogArtiklenDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Email> Emails => Set<Email>();
        public DbSet<Follow> Follows => Set<Follow>();
        public DbSet<Article> Articles => Set<Article>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<Reply> Replies => Set<Reply>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<Bookmark> Bookmarks => Set<Bookmark>();
        public DbSet<ArticleTag> ArticleTags => Set<ArticleTag>();
        public DbSet<UserLike> UserLikes => Set<UserLike>();
        public DbSet<Like> Like => Set<Like>();



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Follow>().HasKey(sc => new { sc.FollowerId, sc.FolloweringId});

            modelBuilder.Entity<Follow>()
                .HasOne<User>(sc => sc.Following)
                .WithMany(s => s.Followings)
                .HasForeignKey(sc => sc.FolloweringId);


            modelBuilder.Entity<Follow>()
                .HasOne<User>(sc => sc.Follower)
                .WithMany(s => s.Followers)
                .HasForeignKey(sc => sc.FollowerId);

            modelBuilder.Entity<Article>()
                .HasOne<User>(s => s.Author)
                .WithMany(u => u.Articles)
                .HasForeignKey(s => s.UserId);


            modelBuilder.Entity<Comment>()
                .HasOne<User>(s => s.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<Comment>()
                .HasOne<Article>(s => s.Article)
                .WithMany(u => u.Comments)
                .HasForeignKey(s => s.ArticleId);

            modelBuilder.Entity<Reply>()
                .HasOne<User>(s => s.User)
                .WithMany(u => u.Replies)
                .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<Reply>()
                .HasOne<Comment>(s => s.Comment)
                .WithMany(u => u.Replies)
                .HasForeignKey(s => s.CommentId);

            modelBuilder.Entity<Bookmark>().HasKey(sc => new { sc.UserId, sc.ArtilceId });

            modelBuilder.Entity<Bookmark>()
                .HasOne<User>(sc => sc.User)
                .WithMany(s => s.Bookmarks)
                .HasForeignKey(sc => sc.UserId);


            modelBuilder.Entity<Bookmark>()
                .HasOne<Article>(sc => sc.Article)
                .WithMany(s => s.Bookmarks)
                .HasForeignKey(sc => sc.ArtilceId);

            modelBuilder.Entity<ArticleTag>().HasKey(sc => new { sc.TagId, sc.ArtilceId });

            modelBuilder.Entity<ArticleTag>()
                .HasOne<Article>(sc => sc.Article)
                .WithMany(s => s.ArticleTags)
                .HasForeignKey(sc => sc.ArtilceId);


            modelBuilder.Entity<ArticleTag>()
                .HasOne<Tag>(sc => sc.Tag)
                .WithMany(s => s.ArticleTags)
                .HasForeignKey(sc => sc.TagId);

            modelBuilder.Entity<UserLike>()
                .HasOne<User>(sc => sc.User)
                .WithMany(s => s.UserLikes)
                .HasForeignKey(sc => sc.UserId);
        }
    }
}
