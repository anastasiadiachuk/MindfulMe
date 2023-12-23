using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;

namespace Services
{
    public class ArticleService : IArticleService
    {
        private readonly ProgramDbContext _dbContext;

        public ArticleService(ProgramDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateArticle(Article article)
        {
            if (_dbContext.Articles.Any(a => a.Title == article.Title))
            {
                throw new ArgumentException("Article with this title already exists");
            }
            _dbContext.Articles.Add(article);
            _dbContext.SaveChanges();
        }

        public Article GetArticleById(int articleId)
        {
            return _dbContext.Articles.FirstOrDefault(a => a.ArticleId == articleId);
        }

        public List<Article> GetAllArticles()
        {
            return _dbContext.Articles.ToList();
        }

        public void UpdateArticle(Article updatedArticle)
        {
            var existingArticle = _dbContext.Articles
                .FirstOrDefault(a => a.ArticleId == updatedArticle.ArticleId);

            if (existingArticle == null) return;
            existingArticle.Title = updatedArticle.Title;
            existingArticle.Description = updatedArticle.Description;
            existingArticle.ImageUrl = updatedArticle.ImageUrl;
            existingArticle.Content = updatedArticle.Content;
            existingArticle.AuthorUsername = updatedArticle.AuthorUsername;

            _dbContext.SaveChanges();
        }

        public void DeleteArticle(int articleId)
        {
            var articleToDelete = _dbContext.Articles.Find(articleId);

            if (articleToDelete == null) return;
            _dbContext.Articles.Remove(articleToDelete);
            _dbContext.SaveChanges();
        }
    }
}
