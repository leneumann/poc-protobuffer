using System;
using System.IO;
using System.Linq.Expressions;
using ProtoBuf.Meta;

namespace src.protobufNet.runTimeTypeModel
{
    public class ProtobufSerializer
    {
        private readonly RuntimeTypeModel _runtimeTypeModel;
        public ProtobufSerializer() : this(RuntimeTypeModel.Default) { }
        public ProtobufSerializer(RuntimeTypeModel runtimeTypeModel)
        {
            if (runtimeTypeModel == null) throw new ArgumentNullException(nameof(runtimeTypeModel));
            _runtimeTypeModel = runtimeTypeModel;
            var cadastro = _runtimeTypeModel.Add(typeof(Cadastro), true);
            cadastro.AddField(1, Path<Cadastro>(x => x.clientes));

            var cliente = _runtimeTypeModel.Add(typeof(Cliente), true);
            cliente.AddField(1, Path<Cliente>(x => x.PrimeiroNome));
            cliente.AddField(2, Path<Cliente>(x => x.UltimoNome));
            cliente.AddField(3, Path<Cliente>(x => x.DataNascimento));
            cliente.AddField(4, Path<Cliente>(x => x.Enderecos));

            var endereco = _runtimeTypeModel.Add(typeof(Endereco), true);
            endereco.AddField(1, Path<Endereco>(x => x.Logradouro));
            endereco.AddField(2, Path<Endereco>(x => x.Complemento));
            endereco.AddField(3, Path<Endereco>(x => x.Bairro));
            endereco.AddField(4, Path<Endereco>(x => x.Cidade));
            endereco.AddField(5, Path<Endereco>(x => x.Pais));
        }

        public static string Path<T>(Expression<Func<T, object>> expression)
        {
            return GetPropertyName(expression);
        }

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

        public byte[] Serialize<T>(T obj) where T : class
        {
            if (obj == null)
                return new byte[] { };
            using (var stream = new MemoryStream())
            {
                _runtimeTypeModel.Serialize(stream, obj);
                return stream.ToArray();
            }
        }
        public object Deserialize<T>(byte[] data) where T : class
        {
            Type type = typeof(T);
            if (data == null)
                return default(T);
            using (var stream = new MemoryStream(data))
            {
                return _runtimeTypeModel.Deserialize(stream, null, type);
            }
        }

    }
}