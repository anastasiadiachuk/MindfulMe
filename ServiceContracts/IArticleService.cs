using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IArticleService
    {
        // Create
        void CreateArticle(Article article);

        // Read
        Article GetArticleById(int articleId);
        List<Article> GetAllArticles();

        // Update
        void UpdateArticle(Article updatedArticle);

        // Delete
        void DeleteArticle(int articleId);
    }
}
