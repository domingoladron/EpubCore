using Penman.EpubSharp.Format;
using System;
using Shouldly;
using Xunit;

namespace Penman.EpubSharp.Tests;

public class NcxNapMapTests
{
    [Fact]
    public void ReorderingNavPointsByPlayOrderShouldUpdatePlayOrder()
    {
        var napMap = new NcxNapMap();
        var nxcNavPoint = new NcxNavPoint
        {
            Id = Guid.NewGuid().ToString("N"),
            NavLabelText = Faker.Lorem.GetFirstWord(),
            ContentSrc = Faker.Lorem.GetFirstWord(),
            PlayOrder = 0

        };

        var nxcNavPoint2 = new NcxNavPoint
        {
            Id = Guid.NewGuid().ToString("N"),
            NavLabelText = Faker.Lorem.GetFirstWord(),
            ContentSrc = Faker.Lorem.GetFirstWord(),
            PlayOrder = 2

        };

        var nxcNavPoint3 = new NcxNavPoint
        {
            Id = Guid.NewGuid().ToString("N"),
            NavLabelText = Faker.Lorem.GetFirstWord(),
            ContentSrc = Faker.Lorem.GetFirstWord(),
            PlayOrder = 1

        };
        napMap.NavPoints.Add(nxcNavPoint);
        napMap.NavPoints.Add(nxcNavPoint3);
        napMap.NavPoints.Add(nxcNavPoint2);


        napMap.ReorderNavPointsPlayOrder();

        var navPointFound = napMap.NavPoints[1];

        navPointFound.PlayOrder.ShouldBe(1);

    }

    [Fact]
    public void ReorderingNavPointsBySamePlayOrderShouldUpdateRightOne()
    {
        var napMap = new NcxNapMap();
        var nxcNavPoint = new NcxNavPoint
        {
            Id = Guid.NewGuid().ToString("N"),
            NavLabelText = Faker.Lorem.GetFirstWord(),
            ContentSrc = Faker.Lorem.GetFirstWord(),
            PlayOrder = 0

        };

        var nxcNavPoint2 = new NcxNavPoint
        {
            Id = Guid.NewGuid().ToString("N"),
            NavLabelText = Faker.Lorem.GetFirstWord(),
            ContentSrc = Faker.Lorem.GetFirstWord(),
            PlayOrder = 1

        };

        var nxcNavPoint3 = new NcxNavPoint
        {
            Id = Guid.NewGuid().ToString("N"),
            NavLabelText = Faker.Lorem.GetFirstWord(),
            ContentSrc = Faker.Lorem.GetFirstWord(),
            PlayOrder = 1
        };
        napMap.NavPoints.Add(nxcNavPoint);
        napMap.NavPoints.Add(nxcNavPoint2);
        napMap.NavPoints.Add(nxcNavPoint3);


        napMap.ReorderNavPointsPlayOrder();

        var navPointFound = napMap.NavPoints[2];

        navPointFound.PlayOrder.ShouldBe(2);

    }
}