using System;
using System.Collections.Generic;

namespace DomainInterface
{
    public interface IInboundRepository
    {
        int AddInbound<T>(T item);
        int DeleteInbound<T>(T item);
        int UpdateInbound<T>(T item);

        int AddInboundItems<T>(T item);
        int DeleteInboundItems<T>(T item);
        int UpdateInboundItems<T>(T item);

       

 
        //IInbound GetNewObjectOfQues();
        //IInboundOption GetNewObjectOfOption();
        //IInbound GetInboundByID(int ID);
        //List<IInbound> GetInboundListByID(int ID);
        //IInbound GetInboundByQues(string strInbound);
        //IList<IInboundOption> GetAllInboundOptions();
        //IInbound GetActiveInbound();
        //IList<IInboundOption> GetPreviuosInbounds();
        //List<IInboundOption> GetActiveInboundAsList();
        //IInbound GetInboundbByFK(int ID);
        //IInboundOption GetInboundOptionByID(int ID);
        //List<IInbound> GetAllInbounds();
    }
}
