using System;
using System.IO;
using System.Linq.Expressions;
using ProtoBuf.Meta;

namespace src.protobufNet.runTimeTypeModel
{
    public interface IMapObject<T>
    {
        IMapProperty<T> MapProperty(Expression<Func<T, object>> expression);
        IMapAllProperties<T> MapAllProperties();
    }

    public interface IMapProperty<T>
    {
        IMapProperty<T> MapProperty(Expression<Func<T, object>> expression);
        IMapObject<Ttype> MapObject<Ttype>();
        RuntimeTypeModel Build();
    }
    public interface IMapAllProperties<T>
    {
        IMapObject<Ttype> MapObject<Ttype>();
        RuntimeTypeModel Build();
    }
    public class ProtoMapBuilder
    {
        public readonly RuntimeTypeModel _runtimeTypeModel;
        private ProtoMapBuilder() : this(RuntimeTypeModel.Default) { }
        private ProtoMapBuilder(RuntimeTypeModel runtimeTypeModel)
        {
            _runtimeTypeModel = runtimeTypeModel;
        }

        public static ProtoMapBuilder New() => new ProtoMapBuilder();

        public IMapObject<T> MapObject<T>() => new ProtoMapObject<T>(this);
    }

    public class ProtoMapObject<T> : IMapObject<T>, IMapProperty<T>,IMapAllProperties<T>
    {
        private readonly ProtoMapBuilder _builder;
        private MetaType _metaType;
        private int _index = 1;
        public ProtoMapObject(ProtoMapBuilder builder)
        {
            _builder = builder;
            _metaType = _builder._runtimeTypeModel.Add(typeof(T), true);
        }

        public IMapObject<Ttype> MapObject<Ttype>() => new ProtoMapObject<Ttype>(_builder);

        public IMapProperty<T> MapProperty(Expression<Func<T, object>> expression)
        {
            _metaType.AddField(_index, GetPropertyName(expression));
            _index++;
            return this;
        }

        public IMapAllProperties<T> MapAllProperties()
        {
            var properties = typeof(T).GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                _metaType.AddField(_index, properties[i].Name);
                _index++;
            }
            return this;
        }
        public RuntimeTypeModel Build() => _builder._runtimeTypeModel;


        static string GetPropertyName(Expression expression)
        {
            if (expression == null) return "";

            if (expression is LambdaExpression)
            {
                expression = ((LambdaExpression)expression).Body;
            }

            if (expression is UnaryExpression)
            {
                expression = ((UnaryExpression)expression).Operand;
            }

            if (expression is MemberExpression)
            {
                dynamic memberExpression = expression;

                var lambdaExpression = (Expression)memberExpression.Expression;

                string prefix;
                if (lambdaExpression != null)
                {
                    prefix = GetPropertyName(lambdaExpression);
                    if (!string.IsNullOrEmpty(prefix))
                    {
                        prefix += ".";
                    }
                }
                else
                {
                    prefix = "";
                }

                var propertyName = memberExpression.Member.Name;

                return prefix + propertyName;
            }

            return "";
        }
    }
}