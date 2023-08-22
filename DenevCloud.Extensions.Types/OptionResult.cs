using System;
using System.Threading.Tasks;

namespace DenevCloud.Extensions.Types;

public readonly struct OptionResult<TResult>
{
    private readonly Option _option { get; }
    private readonly TResult? _result { get; }

    public OptionResult(TResult result)
    {
        _result = result;
        _option = Option.Some;
    }

    public OptionResult()
    {
        _option = Option.None;
    }

    public TResult? Unwrap(bool ThrowIfNone = false)
    {
        if (_option == Option.None)
            if(ThrowIfNone)
                throw new InvalidOperationException("Cannot unwrap a None OptionResult.");
            else
                return default;
        else
            return _result;
    }

    public Option Option => _option;

    public Task<TResult?> UnwrapAsync(bool ThrowIfNone = false)
    {
        Task.Yield();

        if (_option == Option.None)
            if (ThrowIfNone)
                throw new InvalidOperationException("Cannot unwrap a None OptionResult.");
            else
                return Task.FromResult<TResult?>(default);
        else
            return Task.FromResult(_result);
    }

    public static implicit operator bool(OptionResult<TResult> result)
    {
        if(result._option == Option.None)
            return false;
        else
            return true;
    }

    public static implicit operator int(OptionResult<TResult> result)
    {
        if (result._option == Option.None)
            return 0;
        else
            return 1;
    }

    public static implicit operator byte(OptionResult<TResult> result)
    {
        if (result._option == Option.None)
            return 0;
        else
            return 1;
    }

    public static implicit operator uint(OptionResult<TResult> result)
    {
        if (result._option == Option.None)
            return 0;
        else
            return 1;
    }

    public static implicit operator sbyte(OptionResult<TResult> result)
    {
        if (result._option == Option.None)
            return 0;
        else
            return 1;
    }

    public static implicit operator Option(OptionResult<TResult> result)
    {
        return result._option;
    }
}

public readonly struct None<TResult>
{
    public static implicit operator OptionResult<TResult?>(None<TResult?> none)
    {
        return new OptionResult<TResult?>();
    }
}

public readonly struct Some<TResult>
{
    private readonly TResult? _result { get; }

    public Some(TResult? result)
    {
        _result = result;
    }

    public static implicit operator OptionResult<TResult?>(Some<TResult?> some)
    {
        return new OptionResult<TResult?>(some._result);
    }
}

public enum Option : byte
{
    None = 0,
    Some = 1
}