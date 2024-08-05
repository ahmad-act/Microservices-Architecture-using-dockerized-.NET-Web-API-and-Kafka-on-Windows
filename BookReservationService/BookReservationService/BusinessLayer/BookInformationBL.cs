using AutoMapper;
using BookReservationService.DataAccessLayer;
using BookReservationService.DTOs;
using BookReservationService.Kafka;
using BookReservationService.Models;

namespace BookReservationService.BusinessLayer
{
    public class BookReservationBL : IBookReservationBL
    {
        private readonly ILogger<object> _logger;
        private readonly IBookReservationDL _bookInformationDL;
        private readonly IMapper _mapper;
        private readonly KafkaConfig _kafkaConfig;

        public BookReservationBL(ILogger<object> logger, IBookReservationDL bookInformationDL, IMapper mapper, KafkaConfig kafkaConfig)
        {
            _logger = logger;
            _bookInformationDL = bookInformationDL;
            _mapper = mapper;
            _kafkaConfig = kafkaConfig;
        }

        public async Task<List<BookReservationDisplayDto>?> GetBookReservations()
        {
            List<BookReservation>? bookReservations = await _bookInformationDL.GetBookReservations();

            if (bookReservations == null)
            {
                return null;
            }
            else
            {
                return bookReservations.Select(b => _mapper.Map<BookReservationDisplayDto>(b)).ToList();
            }
        }

        public async Task<BookReservationDisplayDto?> GetBookReservation(int id)
        {
            BookReservation? bookInformation = await _bookInformationDL.GetBookReservation(id);

            if (bookInformation == null)
            {
                return null;
            }
            else
            {
                return _mapper.Map<BookReservationDisplayDto>(bookInformation);
            }
        }

        public async Task<BookReservationDisplayDto?> CreateBookReservation(BookReservationUpdateDto bookInformationUpdateDto)
        {
            BookReservation? bookInformation = _mapper.Map<BookReservation>(bookInformationUpdateDto);

            int result = await _bookInformationDL.CreateBookReservation(bookInformation);

            MessageObject messageObject = new MessageObject()
            {
                BookId = bookInformationUpdateDto.BookId,
                Reserved = bookInformation.Reserved
            };

            UpdateBookAvailability(messageObject);

            return _mapper.Map<BookReservationDisplayDto>(bookInformation);
        }

        public async Task<BookReservationDisplayDto?> UpdateBookReservation(int id, BookReservationUpdateDto bookInformationUpdateDto)
        {
            if (bookInformationUpdateDto == null)
            {
                return null;
            }

            var existingBookReservation = await _bookInformationDL.GetBookReservation(id);

            if (existingBookReservation == null)
            {
                return null;
            }

            existingBookReservation.BookId = bookInformationUpdateDto.BookId;
            existingBookReservation.Reserved = bookInformationUpdateDto.Reserved;

            int result = await _bookInformationDL.UpdateBookReservation(existingBookReservation);

            return _mapper.Map<BookReservationDisplayDto>(existingBookReservation);
        }

        public async Task<string> DeleteBookReservation(int id)
        {
            var bookInformation = await _bookInformationDL.GetBookReservation(id);

            if (bookInformation == null)
            {
                return "Book info not found";
            }

            int result = await _bookInformationDL.DeleteBookReservation(bookInformation);

            return string.Empty;
        }

        public async Task<List<BookReservationDisplayDto>?> SearchBookReservations(string searchTerm)
        {
            List<BookReservation>? bookReservations = await _bookInformationDL.SearchBookReservations(searchTerm);

            if (bookReservations == null)
            { 
                return null; 
            }
            else
            {
                return bookReservations.Select(b => _mapper.Map<BookReservationDisplayDto>(b)).ToList();
            }
            
        }

        private void UpdateBookAvailability(MessageObject messageObject)
        {
            _kafkaConfig.Send<MessageObject>(messageObject, _logger);
        }
    }
}
