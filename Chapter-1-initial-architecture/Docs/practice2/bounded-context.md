# Offers

It is responsible for creating offers for particular customers giving same discount. If customer accepts it, then contracts can be prepared for them. Also when for particular customer contracts (contracts passes) expires, then the new renewal offers should be created and sent.

So, on PassExpireEvent, this module create new OfferPrepareEvent, which creates the offer for given customer.

This module has not its own api endpoint, so it interacts with others only via events.

Also there is no implementation of contracts and offers relationship, so it would be easier in earlier steps separate Offers module before implementing it.

Offer domain structure:
```c#
Offer(
    Guid id,
    Guid customerId,
    DateTimeOffset preparedAt,
    decimal discount,
    DateTimeOffset offeredFromDate,
    DateTimeOffset offeredFromTo
)
```

Since there is a strong relationship between Offers and Passes, so I have created separate Fitnet.Abstractions module to avoid circular dependency.