using Core.Interfaces;
using Core.Results.Basic;
using Core.ValueObjects.Regular.Base;

namespace Core.ValueObjects.Regular.Strings;

public abstract record StringValueObject<T>(string Value) : SingleValueObject<string>(Value)
    where T : StringValueObject<T>, ISimpleValueObjectFactory<string, T>
{
    public ValueObjectResult<T> ToLower(StringValueObject<T> value)
    {
        return T.Create(value.Value.ToLower());
    }
    
    public ValueObjectResult<T> ToUpper(StringValueObject<T> value)
    {
        return T.Create(value.Value.ToUpper());
    }
    
    public static ValueObjectResult<T> operator +(StringValueObject<T> a, StringValueObject<T> b)
    {
        return T.Create(a.Value + b.Value);
    }
    
    public static ValueObjectResult<T> operator -(StringValueObject<T> a, StringValueObject<T> b)
    {
        return T.Create(a.Value.Replace(b.Value, "").Trim());
    }
    
    public static ValueObjectResult<T> operator +(StringValueObject<T> a, string b)
    {
        return T.Create(a.Value + b);
    }
    
    public static ValueObjectResult<T> operator -(StringValueObject<T> a, string b)
    {
        return T.Create(a.Value.Replace(b, "").Trim());
    }
    
    public static ValueObjectResult<T> operator +(string a, StringValueObject<T> b)
    {
        return T.Create(a + b.Value);
    }
    
    public static ValueObjectResult<T> operator -(string a, StringValueObject<T> b)
    {
        return T.Create(a.Replace(b.Value, "").Trim());
    }
}