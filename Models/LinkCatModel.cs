namespace LinksApp.Models
{
    public class LinkCatModel
    {
        // sinlge instace of a model objects
        public LinkModel linkModel {get; set;} = new LinkModel();
        public CatModel catModel {get; set;} = new CatModel();

         
         
         //  list versions for holding lists of the model objects (mostly for populating views with all db info)
        public List<LinkModel> LinkModels { get; set; } = new List<LinkModel>();
        public List<CatModel> CatModels { get; set; } = new List<CatModel>();
    }
}