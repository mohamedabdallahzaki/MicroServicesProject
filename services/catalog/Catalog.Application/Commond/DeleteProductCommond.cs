using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Commond
{
    public class DeleteProductCommond:IRequest<bool>
    {
        public string Id { get; set; }

        public DeleteProductCommond(string id)
        {
            Id = id;
        }
    }
}
