using System;

namespace Logger_abstract_factory
{

    // Logger
    // FileLogger And DBLogger
    // FileLogger - TextLogger, ApplicationLogger
    // DbLogger - SQLLogger, OracleLogger
    static class Program
    {
        static void Main(string[] args)
        {
            ILoggerFactory fileFactory = Client.GetFactory(EFactroryType.FileFactory);
            IFileLogger textFileLogger = fileFactory.CreateFileLogger(EFileTypeLogger.Text);
            textFileLogger.LogToFile("logging to text file");
            IFileLogger appFileLogger = fileFactory.CreateFileLogger(EFileTypeLogger.Application);
            appFileLogger.LogToFile("logging to application file");
            ILoggerFactory dbFactory = Client.GetFactory(EFactroryType.DbFactory);
            IDbLogger sqlDbLogger = dbFactory.CreateDbLogger(EDbTypeLogger.Sql);
            sqlDbLogger.LogToDb("logging to sql db");
            IDbLogger oracleDbLogger = dbFactory.CreateDbLogger(EDbTypeLogger.Oracle);
            oracleDbLogger.LogToDb("logging to oracle db");
        }
    }

    // categories
    public interface IFileLogger
    {
        void LogToFile(string message);
    }

    public interface IDbLogger
    {
        void LogToDb(string message);
    }

    public class SqlLogger : IDbLogger
    {
        public void LogToDb(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class OracleLogger : IDbLogger
    {
        public void LogToDb(string message)
        {
            Console.WriteLine(message);
        }
    }
    public class TextLogger : IFileLogger
    {
        public void LogToFile(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class ApplicationLogger : IFileLogger
    {
        public void LogToFile(string message)
        {
            Console.WriteLine(message);
        }
    }

    public enum EFileTypeLogger
    {
        Text,
        Application
    }

    public enum EDbTypeLogger
    {
        Sql,
        Oracle
    }

    public interface ILoggerFactory
    {
        IFileLogger CreateFileLogger(EFileTypeLogger eFileType);
        IDbLogger CreateDbLogger(EDbTypeLogger eDbType);
    }

    public class FileLoggerFactory : ILoggerFactory
    {
        public IDbLogger CreateDbLogger(EDbTypeLogger eDbType)
        {
            return null;
        }

        public IFileLogger CreateFileLogger(EFileTypeLogger eFileType)
        {
            switch (eFileType)
            {
                case EFileTypeLogger.Text:
                    return new TextLogger();
                case EFileTypeLogger.Application:
                    return new ApplicationLogger();
                default:
                    return new TextLogger();
            }
        }
    }

    public class DbLoggerFacotory: ILoggerFactory
    {
        public IDbLogger CreateDbLogger(EDbTypeLogger eDbType)
        {
            switch (eDbType)
            {
                case EDbTypeLogger.Sql:
                    return new SqlLogger();
                case EDbTypeLogger.Oracle:
                    return new OracleLogger();
                default:
                    return new SqlLogger();
            }
        }

        public IFileLogger CreateFileLogger(EFileTypeLogger eFileType)
        {
            return null;
        }
    }

    public enum EFactroryType
    {
        FileFactory,
        DbFactory
    }

    public static class Client
    {
       public static ILoggerFactory GetFactory(EFactroryType type)
        {
            switch (type)
            {
                case EFactroryType.FileFactory:
                    return new FileLoggerFactory();
                case EFactroryType.DbFactory:
                    return new DbLoggerFacotory();
                default:
                    return null;
            }
        }
    }
}
