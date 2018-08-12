using System;
using System.Collections.Generic;
using ExchangeCalendarMerge;
using ExchangeCalendarMerge.ExchangeServiceWrappers;
using Moq;
using NUnit.Framework;

namespace ExchangeCalendarMergeTests
{
    [TestFixture]
    public class ExchangeCalendarMergerTests
    {
        #region SyncAdds

        [Test]
        public void MeetingInCalenderOne_NotInCalenderTwo_GetsSynced()
        {          
            // Arrange
            var moqAppt = new Mock<IEcmAppointment>();
            moqAppt.Setup(x => x.Subject).Returns("Test Apt #1");
            moqAppt.Setup(x => x.Start).Returns(new DateTime(2016,1,1,12,0,0));
            moqAppt.Setup(x => x.Start).Returns(new DateTime(2016,1,1,13,0,0));
            
            var sv1 = new List<IEcmAppointment>
            {
                moqAppt.Object
            };
            var sv2 = new List<IEcmAppointment>{};

            // Act
            var result = ExchangeCalendarMerger.GetApptsToAdd(sv1, sv2, "sv2");

            // Assert
            Assert.That(result.Contains(moqAppt.Object));
        }

        [Test]
        public void MeetingInCalenderOne_CopyInCalenderTwo_DoesNotSync()
        {
            // Arrange
            var moqAppt1 = new Mock<IEcmAppointment>();
            moqAppt1.Setup(x => x.Subject).Returns("Test Apt #1");
            moqAppt1.Setup(x => x.Start).Returns(new DateTime(2016, 1, 1, 12, 0, 0));
            moqAppt1.Setup(x => x.Start).Returns(new DateTime(2016, 1, 1, 13, 0, 0));

            var moqAppt2 = new Mock<IEcmAppointment>();
            moqAppt2.Setup(x => x.Subject).Returns("sv1:Test Apt #1");
            moqAppt2.Setup(x => x.Start).Returns(new DateTime(2016, 1, 1, 12, 0, 0));
            moqAppt2.Setup(x => x.Start).Returns(new DateTime(2016, 1, 1, 13, 0, 0));

            var sv1 = new List<IEcmAppointment> { moqAppt1.Object };
            var sv2 = new List<IEcmAppointment> { moqAppt2.Object };

            // Act
            var result = ExchangeCalendarMerger.GetApptsToAdd(sv1, sv2, "sv2");

            // Assert
            Assert.That(result.Count == 0);
        }

        [Test]
        public void MeetingInCalenderOne_FromCalendarTwo_DoesNotSync()
        {
            // Arrange
            var moqAppt1 = new Mock<IEcmAppointment>();
            moqAppt1.Setup(x => x.Subject).Returns("sv2:Test Apt #1");
            moqAppt1.Setup(x => x.Start).Returns(new DateTime(2016, 1, 1, 12, 0, 0));
            moqAppt1.Setup(x => x.Start).Returns(new DateTime(2016, 1, 1, 13, 0, 0));

            var sv1 = new List<IEcmAppointment> { moqAppt1.Object };
            var sv2 = new List<IEcmAppointment> { };

            // Act
            var result = ExchangeCalendarMerger.GetApptsToAdd(sv1, sv2, "sv2");

            // Assert
            Assert.That(result.Count == 0);
        }

        [Test]
        public void MeetingInBothCalendars_DoesNotSync()
        {
            // Arrange
            var moqAppt1 = new Mock<IEcmAppointment>();
            moqAppt1.Setup(x => x.Subject).Returns("Test Apt #1");
            moqAppt1.Setup(x => x.Start).Returns(new DateTime(2016, 1, 1, 12, 0, 0));
            moqAppt1.Setup(x => x.Start).Returns(new DateTime(2016, 1, 1, 13, 0, 0));

            var moqAppt2 = new Mock<IEcmAppointment>();
            moqAppt2.Setup(x => x.Subject).Returns("Test Apt #1");
            moqAppt2.Setup(x => x.Start).Returns(new DateTime(2016, 1, 1, 12, 0, 0));
            moqAppt2.Setup(x => x.Start).Returns(new DateTime(2016, 1, 1, 13, 0, 0));

            var sv1 = new List<IEcmAppointment> { moqAppt1.Object };
            var sv2 = new List<IEcmAppointment> { moqAppt2.Object };

            // Act
            var result = ExchangeCalendarMerger.GetApptsToAdd(sv1, sv2, "sv2");

            // Assert
            Assert.That(result.Count == 0);
        }
        #endregion

        #region Sync Deletes

        [Test]
        public void MeetingFromServerOne_OnServerTwo_NotServerOne_GetsDeleted()
        {
            // Arrange
            var moqAppt = new Mock<IEcmAppointment>();
            moqAppt.Setup(x => x.Subject).Returns("sv1:Test Apt #1");
            moqAppt.Setup(x => x.Start).Returns(new DateTime(2016, 1, 1, 12, 0, 0));
            moqAppt.Setup(x => x.Start).Returns(new DateTime(2016, 1, 1, 13, 0, 0));

            var sv1 = new List<IEcmAppointment>();            
            var sv2 = new List<IEcmAppointment> { moqAppt.Object };

            // Act
            var result = ExchangeCalendarMerger.GetApptsToDelete(sv1, "sv1", sv2);

            // Assert
            Assert.That(result.Contains(moqAppt.Object));
        }

        [Test]
        public void MeetingInBoth_NoDeletes()
        {
            // Arrange
            var moqAppt = new Mock<IEcmAppointment>();
            moqAppt.Setup(x => x.Subject).Returns("Test Apt #1");
            moqAppt.Setup(x => x.Start).Returns(new DateTime(2016, 1, 1, 12, 0, 0));
            moqAppt.Setup(x => x.Start).Returns(new DateTime(2016, 1, 1, 13, 0, 0));

            var moqAppt2 = new Mock<IEcmAppointment>();
            moqAppt2.Setup(x => x.Subject).Returns("Test Apt #1");
            moqAppt2.Setup(x => x.Start).Returns(new DateTime(2016, 1, 1, 12, 0, 0));
            moqAppt2.Setup(x => x.Start).Returns(new DateTime(2016, 1, 1, 13, 0, 0));

            var sv1 = new List<IEcmAppointment> {moqAppt.Object };
            var sv2 = new List<IEcmAppointment> { moqAppt2.Object };

            // Act
            var result = ExchangeCalendarMerger.GetApptsToDelete(sv1, "sv1", sv2);

            // Assert
            Assert.That(result.Count == 0);
        }
        #endregion
    }
}