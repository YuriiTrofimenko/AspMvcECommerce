using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetMvcECommerce.Domain.EntityController
{
    public class ArticleEC : AbstractEC<Article>
    {
        public ArticleEC(AspNetMvcECommerceEntities _db) : base(_db){
        }
        public IEnumerable<Article> Articles { get { return mDb.Articles;  } }
    }
}
