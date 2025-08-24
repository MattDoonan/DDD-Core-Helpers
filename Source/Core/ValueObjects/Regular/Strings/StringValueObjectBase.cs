using Core.Results.Basic;
using Core.ValueObjects.Regular.Base;

namespace Core.ValueObjects.Regular.Strings;

public abstract class StringValueObjectBase<T>(string value) : ValueObjectBase<string>(value)
    where T : class, IValueObject<string, T>
{
    public ValueObjectResult<T> ToLower(StringValueObjectBase<T> value)
    {
        return T.Create(value.Value.ToLower());
    }
    
    public ValueObjectResult<T> ToUpper(StringValueObjectBase<T> value)
    {
        return T.Create(value.Value.ToUpper());
    }
    
    public static ValueObjectResult<T> operator +(StringValueObjectBase<T> a, StringValueObjectBase<T> b)
    {
        return T.Create(a.Value + b.Value);
    }
    
    public static ValueObjectResult<T> operator -(StringValueObjectBase<T> a, StringValueObjectBase<T> b)
    {
        return T.Create(a.Value.Replace(b.Value, "").Trim());
    }
    
    public static ValueObjectResult<T> operator +(StringValueObjectBase<T> a, string b)
    {
        return T.Create(a.Value + b);
    }
    
    public static ValueObjectResult<T> operator -(StringValueObjectBase<T> a, string b)
    {
        return T.Create(a.Value.Replace(b, "").Trim());
    }
    
    public static ValueObjectResult<T> operator +(string a, StringValueObjectBase<T> b)
    {
        return T.Create(a + b.Value);
    }
    
    public static ValueObjectResult<T> operator -(string a, StringValueObjectBase<T> b)
    {
        return T.Create(a.Replace(b.Value, "").Trim());
    }
}