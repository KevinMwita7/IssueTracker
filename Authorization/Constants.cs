using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace IssueTracker.Authorization {
    public static class Constants {
        public static Dictionary<string, string> CRUD_OPS = new Dictionary<string, string>() {
          { "CreateOperationName",  "Create" },
          { "ReadOperationName",  "Read" },
          { "UpdateOperationName",  "Update" },
          { "DeleteOperationName",  "Delete" },
        };

        public static Dictionary<string, string> ROLES = new Dictionary<string, string>() {
          { "AdministratorsRole", "Administrator" },
          { "ProjectManagerRole", "ProjectManager" }
        };
    }

        public static class CrudOperations
    {
        public static OperationAuthorizationRequirement Create =   
          new OperationAuthorizationRequirement {Name=Constants.CRUD_OPS["CreateOperationName"]};
        public static OperationAuthorizationRequirement Read = 
          new OperationAuthorizationRequirement {Name=Constants.CRUD_OPS["ReadOperationName"]};  
        public static OperationAuthorizationRequirement Update = 
          new OperationAuthorizationRequirement {Name=Constants.CRUD_OPS["UpdateOperationName"]}; 
        public static OperationAuthorizationRequirement Delete = 
          new OperationAuthorizationRequirement {Name=Constants.CRUD_OPS["DeleteOperationName"]};
    }
}